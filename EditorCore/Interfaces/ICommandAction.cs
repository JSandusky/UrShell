using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Interfaces
{
    /// <summary>
    /// Classes marked with this attribute will be considered for scanning to find ICommandActions
    /// </summary>
    //[AttributeUsage(AttributeTargets.Class)]
    //public class ICommandClient : Attribute
    //{
    //}

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ICommandChecker : Attribute
    {
        public ICommandChecker(string cmdName) { CommandName = cmdName; }
        public string CommandName { get; set; }
    }

    /// <summary>
    /// Methods marked with this attribute can be invoked via command handling
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ICommandAction : Attribute
    {
        public ICommandAction(string cmdName) { CommandName = cmdName; }

        public string CommandName { get; set; }
    }

    public static class CommandActionHelper
    {
        public static bool CanDoCommand(string cmd, object testing)
        {
            if (testing == null)
                return false;
            foreach (MethodInfo mi in testing.GetType().GetMethods(BindingFlags.Public))
            {
                if (mi.ReturnType != typeof(bool)) // Method must return a bool
                    continue;

                IEnumerable<ICommandChecker> checkers = mi.GetCustomAttributes<ICommandChecker>();
                if (checkers == null)
                    continue;
                foreach (ICommandChecker checker in checkers)
                {
                    if (checker.CommandName.Equals(cmd))
                    {
                        bool ret = (bool)mi.Invoke(testing, null);
                        if (ret)
                            return true;
                    }
                }
            }
            return false;
        }

        public static bool DoCommand(string cmd, object testing)
        {
            if (testing == null)
                return false;
            if (CanDoCommand(cmd, testing))
            {
                foreach (MethodInfo mi in testing.GetType().GetMethods())
                {
                    IEnumerable<ICommandAction> actions = mi.GetCustomAttributes<ICommandAction>();
                    if (actions == null)
                        continue;
                    foreach (ICommandAction action in actions)
                    {
                        if (action.CommandName.Equals(cmd))
                        {
                            mi.Invoke(testing, null);
                            return true;
                        }
                    }
                }
            }
            return false; // we didn't execute
        }
    }
}
