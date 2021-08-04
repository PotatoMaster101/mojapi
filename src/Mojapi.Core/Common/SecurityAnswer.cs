using System;

namespace Mojapi.Core.Common
{
    /// <summary>
    /// Stores a security answer.
    /// </summary>
    public record SecurityAnswer
    {
        /// <summary>
        /// Gets the ID of the answer.
        /// </summary>
        /// <value>The ID of the answer.</value>
        public long Id { get; }

        /// <summary>
        /// Gets the security answer.
        /// </summary>
        /// <value>The security answer.</value>
        public string Answer { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="SecurityAnswer"/>.
        /// </summary>
        /// <param name="id">The ID of the answer.</param>
        /// <param name="answer">The content of the answer.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="answer"/> is <see langword="null"/> or empty.</exception>
        public SecurityAnswer(long id, string answer)
        {
            if (string.IsNullOrEmpty(answer))
                throw new ArgumentException("Invalid answer", nameof(answer));

            Id = id;
            Answer = answer;
        }

        /// <summary>
        /// Returns a JSON representation of the current object.
        /// </summary>
        /// <param name="idKey">The JSON key for answer ID.</param>
        /// <param name="answerKey">The JSON key for answer content.</param>
        /// <returns>The JSON string representation of the current object.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="idKey"/> or <paramref name="answerKey"/> is <see langword="null"/> or empty.</exception>
        public string ToJsonString(string idKey = "id", string answerKey = "answer")
        {
            if (string.IsNullOrEmpty(idKey))
                throw new ArgumentException("Invalid ID key", nameof(idKey));
            if (string.IsNullOrEmpty(answerKey))
                throw new ArgumentException("Invalid answer key", nameof(answerKey));
            return $@"""{idKey}"":{Id},""{answerKey}"":""{Answer}""";
        }
    }
}
