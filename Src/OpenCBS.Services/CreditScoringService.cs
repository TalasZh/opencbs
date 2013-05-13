// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.CreditScoring;
using OpenCBS.Enums;
using OpenCBS.ExceptionsHandler;
using OpenCBS.Manager;

namespace OpenCBS.Services
{
    public class CreditScoringService : Services
    {
        private readonly CreditScoringManager _manager;
        public CreditScoringService(User user)
        {
            _manager = new CreditScoringManager(user);
        }

        public QuestionList AddQuestion(QuestionList questions)
        {
            using (SqlConnection conn = _manager.GetConnection())
            using (SqlTransaction t = conn.BeginTransaction())
            {
                try
                {
                    QuestionList list = new QuestionList();
                    foreach (Question question in questions.Questions)
                    {
                        list.Questions.Add(_manager.AddQuestion(question, t));
                    }
                    t.Commit();
                    return list;
                }
                catch (Exception)
                {
                    if (t != null) t.Rollback();
                    throw;
                }
            }
        }

        public QuestionList SelectQuestions()
        {
            return _manager.SelectQuestions();
        }

        public void DeleteQuestions(QuestionList questions)
        {
           using (SqlConnection conn = _manager.GetConnection())
           using (SqlTransaction t = conn.BeginTransaction())
           {
               try
               {
                   foreach (Question question in questions.Questions)
                   {
                       _manager.DeleteQuestion(question, t);
                   }
                   t.Commit();
               }
               catch (Exception)
               {
                   if (t != null) t.Rollback();
                   throw;
               }
           }
        }

        public void UpdateQuestions(QuestionList initialList, QuestionList newList)
        {
            using (SqlConnection conn = _manager.GetConnection())
            using (SqlTransaction t = conn.BeginTransaction())
            {
                try
                {
                    // delete outdated questions
                    foreach (Question question in initialList.Questions)
                    {
                        bool notFound = true;
                        foreach (Question question1 in newList.Questions)
                        {
                            if (question == question1)
                                notFound = false;
                        }
                        if (notFound)
                            DeleteQuestion(question);
                    }

                    foreach (Question question in newList.Questions)
                    {
                        if (question.Id > 0)
                        {
                            _manager.UpdateQuestion(question, t);
                        }
                        else
                        {
                            question.Id = _manager.AddQuestion(question, t).Id;
                        }
                    }
                    t.Commit();
                }
                catch (Exception)
                {
                    if (t != null) t.Rollback();
                    throw;
                }
            }
        }

        public void DeleteQuestion(Question question)
        {
           using (SqlConnection conn = _manager.GetConnection())
           using (SqlTransaction t = conn.BeginTransaction())
           {
               try
               {
                   _manager.DeleteQuestion(question, t);
                   t.Commit();
               }
               catch (Exception)
               {
                   if (t != null) t.Rollback();
                   throw;
               }
           }
        }

        public void CheckQuestions(QuestionList questions)
        {
            foreach (Question question in questions.Questions)
            {
                if (string.IsNullOrEmpty(question.Name))
                {
                    throw new ScoringQuestionNameException();
                }

                if (question.Type == OQuestionType.Collection)
                {
                    foreach (Answer answer in question.Answers)
                    {
                        if (string.IsNullOrEmpty(answer.Name))
                        {
                            throw new ScoringQuestionNameException();
                        }
                    }
                }
            }

        }

        public void SaveQuestionValues(QuestionList questions, int loanId)
        {
              using (SqlConnection conn = _manager.GetConnection())
              using (SqlTransaction t = conn.BeginTransaction())
              {
                  try
                  {
                      foreach (Question question in questions.Questions)
                      {
                          _manager.SaveQuestionValues(question, loanId, t);
                      }
                      t.Commit();
                  }
                  catch (Exception)
                  {
                      if (t != null) t.Rollback();
                      throw;
                  }
              }
        }

        public void UpdateQuestionValues(QuestionList questions, int loanId)
        {
            using (SqlConnection conn = _manager.GetConnection())
            using (SqlTransaction t = conn.BeginTransaction())
            {

                try
                {
                    foreach (Question question in questions.Questions)
                    {
                        _manager.UpdateQuestionValues(question, loanId, t);
                    }
                    t.Commit();
                }
                catch (Exception)
                {
                    if (t != null) t.Rollback();
                    throw;
                }
            }
        }

        public QuestionList SelectValues(int loanId)
        {
            return _manager.SelectValues(loanId);
        }
    }
}
