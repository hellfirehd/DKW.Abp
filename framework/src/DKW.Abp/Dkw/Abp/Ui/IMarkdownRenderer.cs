using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dkw.Abp.Ui;

public interface IMarkdownRenderer
{
    String Render(String markdown);
}
