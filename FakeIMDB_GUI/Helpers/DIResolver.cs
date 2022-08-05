using FakeIMDB_GUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace FakeIMDB_GUI.Helpers
{
    public class DIResolver : MarkupExtension
    {
        public static Func<Type, object> Resolver;

        public Type Type { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Resolver?.Invoke(Type);
        }
    }
}
