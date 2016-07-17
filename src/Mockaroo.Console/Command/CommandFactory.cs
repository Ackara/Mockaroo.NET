using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mockaroo.Command
{
    public class CommandFactory
    {
        public CommandFactory()
        {
            LoadCommandTypes();
        }

        public ICommand CreateInstance(object options)
        {
            if (options != null)
                try
                {
                    string key = options.GetType().FullName;
                    return (ICommand)Activator.CreateInstance(_commandTypes[key]);
                }
                catch (KeyNotFoundException) { }

            return new NullCommand();
        }

        #region Private Members

        private IDictionary<string, Type> _commandTypes;

        private void LoadCommandTypes()
        {
            _commandTypes = new Dictionary<string, Type>();

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var bindingInfo = type.GetCustomAttribute<OptionsBindingAttribute>();
                if (bindingInfo != null
                    && !type.IsAbstract
                    && type.GetInterface(typeof(ICommand).Name) != null)
                { _commandTypes.Add(bindingInfo.OptionType.FullName, type); }
            }
        }

        #endregion Private Members
    }
}