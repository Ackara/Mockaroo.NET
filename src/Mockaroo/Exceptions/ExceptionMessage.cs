using System;

namespace Acklann.Mockaroo.Exceptions
{
    //
    // Summary:
    //     Messages to globalize.
    internal class ExceptionMessage
    {

        public static string NoFieldsWasAssigned(Type template)
        {
            return $"Cannot convert {template.Name} to {nameof(Schema)} because {template.Name} do not have any public .";
        }

        public static string HaveNoConstructor(Type type)
        {
            return $"Cannot convert {type.Name} to {nameof(Schema)} because {type.Name} do not have a constructor with zero parameters.";
        }

        public static string CannotMapToDataType(Type type)
        {
            return $"Cannot mock a member of type <{type.Name}>.";
        }

        public static string ExpressionWasNotAProperty(string expr)
        {
            return $"Expression '{expr}' must refer to a field or property.";
        }
    }
}