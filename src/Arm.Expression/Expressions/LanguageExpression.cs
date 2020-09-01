// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
namespace Arm.Expression.Expressions
{
    public abstract class LanguageExpression
    {
        /// <summary>
        /// Gets or sets the parent function expression.
        /// </summary>
        public FunctionExpression Parent { get; set; }        
    }
}
