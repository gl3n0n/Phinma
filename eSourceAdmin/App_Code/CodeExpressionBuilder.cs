using System;
using System.Web;
using System.CodeDom;
using System.Web.UI;
using System.Web.Compilation;

namespace Ebid.Web.Compilation
{
    [ExpressionPrefix("Code")]
    public class CodeExpressionBuilder : ExpressionBuilder
    {
        public override CodeExpression GetCodeExpression(BoundPropertyEntry entry,
           object parsedData, ExpressionBuilderContext context)
        {
            return new CodeSnippetExpression(entry.Expression);
        }
    }

}
