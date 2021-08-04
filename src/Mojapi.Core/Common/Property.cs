using System;
using System.Text.Json;

namespace Mojapi.Core.Common
{
    /// <summary>
    /// Stores the property values.
    /// </summary>
    public record Property
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the property.
        /// </summary>
        /// <value>The value of the property.</value>
        public string Value { get; }

        /// <summary>
        /// Gets the signature of the property.
        /// </summary>
        /// <value>The signature of the property.</value>
        public string Signature { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="Property"/>.
        /// </summary>
        /// <param name="json">The JSON containing the property data.</param>
        /// <param name="nameKey">The JSON key for property name.</param>
        /// <param name="valueKey">The JSON key for property value.</param>
        /// <param name="sigKey">The JSON key for property signature. The signature is optional.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="json"/> does not contain one of the key.</exception>
        public Property(JsonElement json, string nameKey = "name", string valueKey = "value", string sigKey = "signature")
        {
            var checkName = json.TryGetProperty(nameKey, out var name);
            var checkVal = json.TryGetProperty(valueKey, out var value);
            var checkSig = json.TryGetProperty(sigKey, out var signature);
            if (!checkName || !checkVal)
                throw new ArgumentException("Invalid json", nameof(json));

            Name = name.GetString();
            Value = value.GetString();
            if (checkSig)
                Signature = signature.GetString();
        }
    }
}
