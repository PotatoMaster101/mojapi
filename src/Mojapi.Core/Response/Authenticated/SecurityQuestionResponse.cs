using System.Collections.Generic;
using Mojapi.Core.Common;

namespace Mojapi.Core.Response.Authenticated
{
    /// <summary>
    /// Represents a response from the security question endpoint.
    /// </summary>
    public class SecurityQuestionResponse : BaseResponse
    {
        /// <summary>
        /// Gets the collection of answer ID and the questions.
        /// </summary>
        /// <value>The collection of answer ID and the questions.</value>
        public IDictionary<long, SecurityQuestion> Questions { get; init; }
    }
}
