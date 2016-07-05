using System;

namespace Mockaroo.Commands
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class OptionsBindingAttribute : Attribute
    {
        public OptionsBindingAttribute(Type typeOfOption)
        {
            OptionType = typeOfOption;
        }

        public readonly Type OptionType;
    }
}