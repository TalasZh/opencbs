using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Octopus.Enums;
using Octopus.Shared.Settings;
using Timer=System.Timers.Timer;

namespace Octopus.Reports.Forms
{
    [ComVisible(true)]
    public partial class ReportBrowserControl : UserControl
    {
        private class ReportTimer : Timer
        {
            public int ElapsedFromStart { get; set; }
            public HtmlElement Element { get; set; }
        }

        private HtmlElement _lastSelected;
        private OReportSortOrder _sortOrder = OReportSortOrder.Alphabet;
        private readonly string[] _tags;
        private string _tag;
        private readonly Dictionary<string, Timer> _timers = new Dictionary<string, Timer>();

        public delegate void ExceptionHandler(Exception e);

        public event ExceptionHandler Exception;

        private void FireException(Exception e)
        {
            if (Exception != null) Exception(e);
        }

        private static readonly string GuidPattern = string.Join(@"\-", new[]
        {
            "[a-f0-9]{8}",
            "[a-f0-9]{4}",
            "[a-f0-9]{4}",
            "[a-f0-9]{4}",
            "[a-f0-9]{12}"
        });

        private static readonly string StarGuidPattern = string.Format(@"^star\-({0})$", GuidPattern);

        internal class ReportWorker : BackgroundWorker
        {
            public Report Report { get; set; }
        }
        
        public ReportBrowserControl()
        {
            InitializeComponent();
            wbReports.ObjectForScripting = this;
            wbReports.DocumentCompleted += DocumentCompleted;

            _tags = Service.GetTags();
        }

        public void LoadDocument()
        {
            LoadDocument(Resource.TagStar, UserSettings.GetReportSortOrder());
        }

        private void LoadDocument(string tag, OReportSortOrder sortOrder)
        {
            if(tag.Equals(_tag) && _sortOrder == sortOrder) return;

            _tag = tag;
            _sortOrder = sortOrder;

            try
            {
                // Load page template
                string dir = GetAppDir();
                dir = Path.Combine(dir, "Template");
                dir = Path.Combine(dir, "Reports");
                string languageCode = CultureInfo.CreateSpecificCulture(UserSettings.Language).TwoLetterISOLanguageName;
                string file = string.Format("page.{0}.tpl.html", languageCode);
                file = Path.Combine(dir, file);
                file = File.Exists(file) ? file : Path.Combine(dir, "page.tpl.html");
                string tpl = File.ReadAllText(file, Encoding.UTF8);

                // Load report div template
                file = Path.Combine(dir, "report.tpl.html");
                string tplReport = File.ReadAllText(file, Encoding.UTF8);

                // Construct an html page from parts
                StringBuilder sb = new StringBuilder(20*1024);
                foreach (Report report in Service.GetReports(_tag, _sortOrder))
                {
                    string[] parts = new[]
                                         {
                                             report.Guid.ToString(),
                                             report.Title,
                                             report.Description,
                                             report.Starred ? "star on" : "star off",
                                             0 == report.OpenCount ? "" : report.OpenCount.ToString()
                                         };
                    sb.Append(string.Format(tplReport, parts));
                }

                file = Path.Combine(dir, "tag.tpl.html");
                string tplTag = File.ReadAllText(file, Encoding.UTF8);
                string[] tagsFormated = _tags
                    .Select(t => string.Format(tplTag, t))
                    .ToArray();

                string tagsElement = string.Join(string.Empty, tagsFormated);

                string fileCss = Path.Combine(dir, "style.css");
                string bodyId = _tag;
                string bodyClass = "sort-by-" + _sortOrder.ToString().ToLower();
                string[] partsPage = new[]
                                         {
                                             fileCss,
                                             bodyId,
                                             bodyClass,
                                             sb.ToString(),
                                             wbReports.Version.ToString(),
                                             tagsElement
                                         };
                string html = string.Format(tpl, partsPage);
                file = Path.Combine(dir, "reports.html");
                File.WriteAllText(file, html);

                // Now load the page into the web browser
                // Once it's loaded DocumentCompleted is invoked
                wbReports.Url = new Uri(file, UriKind.Absolute);
            }
            finally { _lastSelected = null; }                        
        }

        private static string GetAppDir()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        private void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Debug.Assert(wbReports.Document != null, "Document is null");
            Debug.Assert(wbReports.Document.Body != null, "Body is null");
            
            HtmlElement el;
            foreach (Report report in Service.GetReports(_tag, _sortOrder))
            {
                el = wbReports.Document.GetElementById(report.Guid.ToString());
                Debug.Assert(el != null, "Report element not found");
                el.Click += ReportItem_Click;
                el.DoubleClick += ReportItem_DoubleClick;

                el = wbReports.Document.GetElementById("star-" + report.Guid);
                Debug.Assert(el != null, "Star element not found");
                el.Click += StarClicked;
            }

            wbReports.Document.Body.Click += OnWhitespaceClick;

            el = GetElementById("sort-by-popularity");
            Debug.Assert(el != null, "Popularity link not found");
            el.Click += LinkPopularityClicked;

            el = GetElementById("sort-by-alphabet");
            Debug.Assert(el != null, "Alphabet link not found");
            el.Click += LinkAlphabetClicked;

            el = GetElementById("tags");
            Debug.Assert(el != null, "Tags element should exist");
            foreach (HtmlElement child in el.Children)
            {
                string currentTag = child.InnerText;
                child.Click += TagClicked;
                child.SetAttribute("className", currentTag.Equals(_tag) ? "tag-selected sort" : "sort");
            }
        }

        private void TagClicked(object sender, HtmlElementEventArgs eventArgs)
        {
            eventArgs.BubbleEvent = false;
            HtmlElement tagSpan = GetElementFromPoint(eventArgs.MousePosition);
            if (tagSpan == null) return;
            
            string tag = tagSpan.InnerText;
            LoadDocument(tag, _sortOrder);
        }

        private HtmlElement GetElementFromPoint(System.Drawing.Point point)
        {
            Debug.Assert(wbReports.Document != null, "Document is null");
            return wbReports.Document.GetElementFromPoint(point);
        }

        private HtmlElement GetElementById(string id)
        {
            Debug.Assert(wbReports.Document != null, "Document is null");
            return wbReports.Document.GetElementById(id);
        }

        private static ReportService Service
        {
            get
            {
                return ReportService.GetInstance();
            }
        }

        private void LinkAlphabetClicked(object sender, HtmlElementEventArgs e)
        {
            if (OReportSortOrder.Alphabet == _sortOrder) return;
            UserSettings.SetReportSortOrder(OReportSortOrder.Alphabet);
            LoadDocument(_tag, OReportSortOrder.Alphabet);
        }

        private void LinkPopularityClicked(object sender, HtmlElementEventArgs e)
        {
            if (OReportSortOrder.Popularity == _sortOrder) return;
            UserSettings.SetReportSortOrder(OReportSortOrder.Popularity);
            LoadDocument(_tag, OReportSortOrder.Popularity);
        }

        private void StarClicked(object sender, HtmlElementEventArgs e)
        {
            Debug.Assert(wbReports.Document != null, "Document is null");
            HtmlElement el = GetElementFromPoint(e.ClientMousePosition);
            if (null == el) return;

            Match match = Regex.Match(el.Id, StarGuidPattern);
            Debug.Assert(match.Success, "Star GUID does not match");
            Debug.Assert(2 == match.Groups.Count, "Invalid # of match groups");

            string guid = match.Groups[1].ToString();
            Report report = Service.GetReport(guid);
            Debug.Assert(report != null, "Report is not found");

            report.Starred = !report.Starred;

            string className = report.Starred ? "star on" : "star off";
            el.SetAttribute("className", className);

            // Hide if we are on the Starred page and the report
            // has been unstarred.
            if (_tag == Resource.TagStar && !report.Starred)
            {
                el = GetElementById("report-" + guid);
                el.SetAttribute("className", "report off");
            }

            Service.SetStarred(report.Name, report.Starred);
        }

        private void ReportItem_DoubleClick(object sender, HtmlElementEventArgs e)
        {
            HtmlElement el = wbReports.Document != null
                                 ? wbReports.Document.GetElementFromPoint(e.ClientMousePosition)
                                 : null;
            string guid = el != null ? el.Id : string.Empty;
            Report ri = ReportService.GetInstance().GetReport(new Guid(guid));
            
            if (ri != null && !_timers.ContainsKey(guid))
            {
                bool showReport = true;
                if (ri.Params.Count > 0)
                {
                    try
                    {
                        ReportParamsForm frm = new ReportParamsForm(ri.Params, ri.Title);
                        if (frm.ShowDialog() != DialogResult.OK)
                        {
                            showReport = false;
                        }
                    }
                    catch (Exception error)
                    {
                        FireException(error);
                        showReport = false;
                    }
                }
                if (showReport)
                {
                    SetItemLoading(guid);
                    StartTimer(guid);
                    _lastSelected = null;
                    BackgroundWorker bwReportLoader = new ReportWorker 
                    { 
                        WorkerReportsProgress = true
                        , Report = ri 
                        , WorkerSupportsCancellation = true
                    };
                    bwReportLoader.DoWork += OnLoadReport;
                    bwReportLoader.RunWorkerCompleted += OnReportLoaded;
                    bwReportLoader.RunWorkerAsync(ri);
                }
            }
        }

        private static void OnLoadReport(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            Report report = e.Argument as Report;
            Debug.Assert(report != null, "Report is null");
            ReportService.GetInstance().LoadReport(report);

            if (bw != null)
                bw.ReportProgress(100);
            e.Result = report;
        }

        private void OnReportLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            ReportWorker rw = sender as ReportWorker;

            if (rw != null && rw.Report != null)
            {
                if (null == _lastSelected)
                    _lastSelected = _SetItemSelected(rw.Report.Guid);
                else
                    SetItemUnselected(rw.Report.Guid);

                StopTimer(rw.Report.Guid.ToString());
            }

            if (e.Error != null)
            {
                FireException(e.Error);
                return;
            }
            if (e.Cancelled) return;

            Report report = e.Result as Report;
            Debug.Assert(report != null, "Report is null");

            try
            {
                report.OpenCount++;
                report.SaveOpenCount();
                UpdateOpenCount(report);
                ReportViewerForm frm = new ReportViewerForm(report);
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ReportItem_Click(object sender, HtmlElementEventArgs e)
        {
            HtmlElement el = GetItem(e.ClientMousePosition);
            if (el != null)
            {
                if (!_IsItemLoading(el.Id))
                {
                    SetItemUnselected(_lastSelected);
                    _lastSelected = _SetItemSelected(el.Id);
                }
            }
            e.BubbleEvent = false;
        }

        private HtmlElement GetItem(string guid)
        {
            string id = string.Format("report-{0}", guid);
            return wbReports.Document != null ? wbReports.Document.GetElementById(id) : null;
        }

        private HtmlElement GetItem(System.Drawing.Point point)
        {
            return wbReports.Document != null ? wbReports.Document.GetElementFromPoint(point) : null;
        }

        private static HtmlElement _SetItemSelected(HtmlElement el)
        {
            if (el != null)
                el.SetAttribute("className", "report selected");
            return el;
        }

        private HtmlElement _SetItemSelected(string guid)
        {
            HtmlElement el = GetItem(guid);
            return _SetItemSelected(el);
        }

        private HtmlElement _SetItemSelected(Guid guid)
        {
            return _SetItemSelected(guid.ToString());
        }

        private static void SetItemUnselected(HtmlElement el)
        {
            if (el != null)
                el.SetAttribute("className", "report");
        }

        private void SetItemUnselected(string guid)
        {
            HtmlElement el = GetItem(guid);
            SetItemUnselected(el);
        }

        private void SetItemUnselected(Guid guid)
        {
            SetItemUnselected(guid.ToString());
        }

        private static void SetItemLoading(HtmlElement el)
        {
            if (el != null)
                el.SetAttribute("className", "report loading");
        }

        private void SetItemLoading(string guid)
        {
            HtmlElement el = GetItem(guid);
            SetItemLoading(el);
        }

        private void StartTimer(string id)
        {
            string elid = string.Format("timer-{0}", id);
            ReportTimer t = new ReportTimer
            {
                Interval = 100
                , Enabled = false
                , ElapsedFromStart = 0
                , Element = wbReports.Document != null ? wbReports.Document.GetElementById(elid) : null
            };
            t.Elapsed += (sender, e) =>
            {
                t.ElapsedFromStart += 100;
                if (null == t.Element) return;
                double seconds = (double) t.ElapsedFromStart/1000;
                t.Element.InnerHtml = string.Format("{0:0.0} sec", seconds);
            };
            _timers.Add(id, t);
            t.Start();
        }

        private void StopTimer(string id)
        {
            Timer t = _timers[id];
            if (null == t) return;
            t.Stop();
            _timers.Remove(id);
        }

        private static bool _IsItemLoading(HtmlElement el)
        {
            if (null == el) return false;
            string className = el.GetAttribute("className");
            return className.IndexOf("loading") > -1;
        }

        private bool _IsItemLoading(string guid)
        {
            return _IsItemLoading(GetItem(guid));
        }

        private void OnWhitespaceClick(object sender, HtmlElementEventArgs e)
        {
            if (_lastSelected != null)
            {
                _lastSelected.SetAttribute("className", "report");
            }
            _lastSelected = null;
        }

        private void UpdateOpenCount(Report report)
        {
            HtmlElement el = GetElementById(string.Format("open-count-{0}", report.Guid));
            Debug.Assert(el != null, "Open count failed: element not found");
            el.InnerHtml = 0 == report.OpenCount ? "" : report.OpenCount.ToString();
        }
    }
}
