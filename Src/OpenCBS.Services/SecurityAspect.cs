// LICENSE PLACEHOLDER

using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;
using OpenCBS.CoreDomain;
using OpenCBS.ExceptionsHandler;

namespace OpenCBS.Services
{
    internal class SecurityAspect : IMessageSink
    {
        internal SecurityAspect(IMessageSink next)
        {
            m_next = next;
        }

        #region Private Vars

        private IMessageSink m_next;
        
        #endregion // Private Vars

        #region IMessageSink implementation

        public IMessageSink NextSink
        {
            get { return m_next; }
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            Preprocess(msg);
            IMessage returnMethod = m_next.SyncProcessMessage(msg);
            return returnMethod;
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new InvalidOperationException();
        }

        #endregion //IMessageSink implementation
        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }
        #region Helper methods
        private void Preprocess(IMessage msg)
        {
            // We only want to process method calls
            if (!(msg is IMethodMessage)) return;

            IMethodMessage call = msg as IMethodMessage;
            Type callType = GetType(call.MethodBase.ReflectedType.FullName); 
            string callStr = callType.Name + "." + call.MethodName;
            Console.WriteLine(@"Security validating : {0} for {1}", callStr, Environment.UserName);

            if (!User.CurrentUser.UserRole.IsActionAllowed(new ActionItemObject(callType.Name, call.MethodName)))
                throw new OctopusRoleDeleteException(OctopusRoleDeleteExceptionsEnum.ActionProhibited, call.MethodName);
            
            // call some security validating code
        }

        #endregion Helpers
    }

    public class SecurityProperty : IContextProperty, IContributeObjectSink
    {
        #region IContributeObjectSink implementation
        
        public IMessageSink GetObjectSink(MarshalByRefObject o, IMessageSink next)
        {
            return new SecurityAspect(next);
        }
        
        #endregion // IContributeObjectSink implementation

        #region IContextProperty implementation
        
        public string Name
        {
            get { return "SecurityProperty"; }
        }
        
        public void Freeze(Context newContext)
        {
        }

        public bool IsNewContextOK(Context newCtx)
        {
            return true;
        }

        #endregion //IContextProperty implementation
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class SecurityAttribute : ContextAttribute
    {
        public SecurityAttribute() : base("Security") { }
        public override void GetPropertiesForNewContext(IConstructionCallMessage ccm)
        {
            ccm.ContextProperties.Add(new SecurityProperty());
        }
    }
} 
