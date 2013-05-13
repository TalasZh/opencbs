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
                throw new OpenCbsRoleDeleteException(OpenCbsRoleDeleteExceptionsEnum.ActionProhibited, call.MethodName);
            
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
