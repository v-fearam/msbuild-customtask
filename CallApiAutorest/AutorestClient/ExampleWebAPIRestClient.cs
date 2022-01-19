// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using myClient.Models;

namespace myClient
{
    internal partial class ExampleWebAPIRestClient
    {
        private Uri endpoint;
        private ClientDiagnostics _clientDiagnostics;
        private HttpPipeline _pipeline;

        /// <summary> Initializes a new instance of ExampleWebAPIRestClient. </summary>
        /// <param name="clientDiagnostics"> The handler for diagnostic messaging in the client. </param>
        /// <param name="pipeline"> The HTTP pipeline for sending and receiving REST requests and responses. </param>
        /// <param name="endpoint"> server parameter. </param>
        public ExampleWebAPIRestClient(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, Uri endpoint = null)
        {
            this.endpoint = endpoint ?? new Uri("");
            _clientDiagnostics = clientDiagnostics;
            _pipeline = pipeline;
        }

        internal HttpMessage CreateWeatherForecastRequest()
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.Reset(endpoint);
            uri.AppendPath("/WeatherForecast", false);
            request.Uri = uri;
            request.Headers.Add("Accept", "application/json, text/json");
            return message;
        }

        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async Task<Response<IReadOnlyList<WeatherForecast>>> WeatherForecastAsync(CancellationToken cancellationToken = default)
        {
            using var message = CreateWeatherForecastRequest();
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        IReadOnlyList<WeatherForecast> value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        List<WeatherForecast> array = new List<WeatherForecast>();
                        foreach (var item in document.RootElement.EnumerateArray())
                        {
                            array.Add(Models.WeatherForecast.DeserializeWeatherForecast(item));
                        }
                        value = array;
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await _clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response<IReadOnlyList<WeatherForecast>> WeatherForecast(CancellationToken cancellationToken = default)
        {
            using var message = CreateWeatherForecastRequest();
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        IReadOnlyList<WeatherForecast> value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        List<WeatherForecast> array = new List<WeatherForecast>();
                        foreach (var item in document.RootElement.EnumerateArray())
                        {
                            array.Add(Models.WeatherForecast.DeserializeWeatherForecast(item));
                        }
                        value = array;
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw _clientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }
    }
}