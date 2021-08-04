using System;
using System.Text.Json;

namespace Mojapi.Core.Common
{
    /// <summary>
    /// Stores a security question.
    /// </summary>
    public record SecurityQuestion
    {
        /// <summary>
        /// Gets the ID of the question.
        /// </summary>
        /// <value>The ID of the question.</value>
        public long Id { get; }

        /// <summary>
        /// Gets the question.
        /// </summary>
        /// <value>The question.</value>
        public string Question { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="SecurityQuestion"/>.
        /// </summary>
        /// <param name="json">The JSON containing the security question.</param>
        /// <param name="idKey">The JSON key for question ID.</param>
        /// <param name="questionKey">The JSON key for question content.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="json"/> does not contain one of the key.</exception>
        public SecurityQuestion(JsonElement json, string idKey = "id", string questionKey = "question")
        {
            var checkId = json.TryGetProperty(idKey, out var id);
            var checkQues = json.TryGetProperty(questionKey, out var question);
            if (!checkId || !checkQues)
                throw new ArgumentException("Invalid json", nameof(json));

            Id = id.GetInt64();
            Question = question.GetString();
        }
    }
}
