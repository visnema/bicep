// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Arm.Expression.Expressions;
using Arm.Expression.Parser;

namespace Bicep.Decompiler
{
    public class ArmExpressionsProvider : IExpressionsProvider
    {
        private readonly ExpressionSerializer serializer = new ExpressionSerializer();

        public bool IsLanguageExpression(string value)
            => ExpressionsEngine.IsLanguageExpression(value);

        public LanguageExpression ParseLanguageExpression(string value)
            => ExpressionsEngine.ParseLanguageExpression(value);

        public string SerializeExpression(LanguageExpression expression)
            => serializer.SerializeExpression(expression);
    }
}