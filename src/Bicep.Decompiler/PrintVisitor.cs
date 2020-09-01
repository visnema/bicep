// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Collections.Generic;
using System.Text;
using Bicep.Core.Parser;
using Bicep.Core.Syntax;

namespace Bicep.Decompiler
{
    public class PrintVisitor : SyntaxVisitor
    {
        private int depth = 0;
        private readonly StringBuilder buffer;

        private void PrintSpace()
        {
            buffer.Append(" ");
        }

        private void PrintIndent()
        {
            for (var i = 0; i < depth; i++)
            {
                buffer.Append("  ");
            }
        }

        public static string Print(ProgramSyntax syntax)
        {
            var buffer = new StringBuilder();
            var visitor = new PrintVisitor(buffer);
            visitor.Visit(syntax);

            return buffer.ToString();
        }

        public override void VisitToken(Token token)
        {
            buffer.Append(token.Text);
        }

        private PrintVisitor(StringBuilder buffer)
        {
            this.buffer = buffer;
        }

        public override void VisitFunctionArgumentSyntax(FunctionArgumentSyntax syntax)
        {
            Visit(syntax.Expression);
            Visit(syntax.Comma);
            if (syntax.Comma != null)
            {
                PrintSpace();
            }
        }

        public override void VisitObjectPropertySyntax(ObjectPropertySyntax syntax)
        {
            PrintIndent();
            Visit(syntax.Key);
            Visit(syntax.Colon);
            PrintSpace();
            Visit(syntax.Value);
            VisitTokens(syntax.NewLines);
        }

        public override void VisitObjectSyntax(ObjectSyntax syntax)
        {
            Visit(syntax.OpenBrace);
            VisitTokens(syntax.NewLines);

            depth++;
            foreach (var item in syntax.Properties)
            {
                Visit(item);
            }
            depth--;

            PrintIndent();
            Visit(syntax.CloseBrace);
        }

        public override void VisitArrayItemSyntax(ArrayItemSyntax syntax)
        {
            PrintIndent();
            Visit(syntax.Value);
            VisitTokens(syntax.NewLines);
        }

        public override void VisitArraySyntax(ArraySyntax syntax)
        {
            Visit(syntax.OpenBracket);
            VisitTokens(syntax.NewLines);

            depth++;
            foreach (var item in syntax.Items)
            {
                Visit(item);
            }
            depth--;

            PrintIndent();
            Visit(syntax.CloseBracket);
        }

        public override void VisitParameterDeclarationSyntax(ParameterDeclarationSyntax syntax)
        {
            Visit(syntax.ParameterKeyword);
            PrintSpace();
            Visit(syntax.Name);
            PrintSpace();
            Visit(syntax.Type);
            PrintSpace();
            Visit(syntax.Modifier);
        }

        public override void VisitParameterDefaultValueSyntax(ParameterDefaultValueSyntax syntax)
        {
            Visit(syntax.AssignmentToken);
            PrintSpace();
            Visit(syntax.DefaultValue);
        }

        public override void VisitVariableDeclarationSyntax(VariableDeclarationSyntax syntax)
        {
            Visit(syntax.VariableKeyword);
            PrintSpace();
            Visit(syntax.Name);
            PrintSpace();
            Visit(syntax.Assignment);
            PrintSpace();
            Visit(syntax.Value);
        }

        public override void VisitResourceDeclarationSyntax(ResourceDeclarationSyntax syntax)
        {
            Visit(syntax.ResourceKeyword);
            PrintSpace();
            Visit(syntax.Name);
            PrintSpace();
            Visit(syntax.Type);
            PrintSpace();
            Visit(syntax.Assignment);
            PrintSpace();
            Visit(syntax.Body);
        }

        public override void VisitOutputDeclarationSyntax(OutputDeclarationSyntax syntax)
        {
            Visit(syntax.OutputKeyword);
            PrintSpace();
            Visit(syntax.Name);
            PrintSpace();
            Visit(syntax.Type);
            PrintSpace();
            Visit(syntax.Assignment);
            PrintSpace();
            Visit(syntax.Value);
        }
    }
}
