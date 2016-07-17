using System;

namespace Mockaroo.Command
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