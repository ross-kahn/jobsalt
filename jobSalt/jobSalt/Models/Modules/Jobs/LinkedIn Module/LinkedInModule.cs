using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.OAuth;

namespace jobSalt.Models
{
    public class LinkedInModule
    {
        /**

        private ServiceProviderDescription GetServiceDescription()
        {
            return new ServiceProviderDescription
            {
                AccessTokenEndpoint = new MessageReceivingEndpoint("https://api.linkedin.com/uas/oauth/accessToken", HttpDeliveryMethods.PostRequest),
                RequestTokenEndpoint = new MessageReceivingEndpoint("https://api.linkedin.com/uas/oauth/requestToken", HttpDeliveryMethods.PostRequest),
                UserAuthorizationEndpoint = new MessageReceivingEndpoint("https://www.linkedin.com/uas/oauth/authorize", HttpDeliveryMethods.PostRequest),
                TamperProtectionElements = new ITamperProtectionChannelBindingElement[] { new HmacSha1SigningBindingElement() },
                ProtocolVersion = ProtocolVersion.V10a
            };
        }

        public ActionResult StartOAuth()
        {
            var serviceProvider = GetServiceDescription();
            var consumer = new WebConsumer(serviceProvider, _tokenManager);

            // Url to redirect to
            var authUrl = new Uri(Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/OAuthCallBack");

            // request access
            consumer.Channel.Send(consumer.PrepareRequestUserAuthorization(authUrl, null, null));

            // This will not get hit!
            return null;
        }

        public ActionResult Test2()
        {
            // Process result from linked in
            var LiServiceProvider = GetServiceDescription();
            var linkedIn = new WebConsumer(LiServiceProvider, _tokenManager);
            var accessToken = GetAccessTokenForUser();

            // Retrieve the user's profile information
            var endpoint = new MessageReceivingEndpoint("http://api.linkedin.com/v1/people/~", HttpDeliveryMethods.GetRequest);
            var request = linkedIn.PrepareAuthorizedRequest(endpoint, accessToken);
            var response = request.GetResponse();
            ViewBag.Result = (new StreamReader(response.GetResponseStream())).ReadToEnd();

            return View();
        }
        **/
    }

    /** 
     * Author: Matthew Shapiro
     * Source: http://scatteredcode.wordpress.com/2011/12/01/dotnetopenauth-oauth-and-mvc-for-dummies/
     * 
     * The token manager is a class which DotNetOpenAuth utilizes to store and 
     * retrieve the consumer key, consumer secret, and a token secret for a 
     * given access key. Since how you will store the user access tokens and token 
     * secrets will vary project to project, DotNetOpenAuth assumes you will 
     * create your own token storage and retrieval mechanism by implementing
     * the IConsumerTokenManager interface.
     **/
    public class InMemoryTokenManager : IConsumerTokenManager, IOpenIdOAuthTokenManager
    {
        private Dictionary<string, string> tokensAndSecrets = new Dictionary<string, string>();

        public InMemoryTokenManager(string consumerKey, string consumerSecret)
        {
            if (String.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
        }

        public string ConsumerKey { get; private set; }

        public string ConsumerSecret { get; private set; }

        #region ITokenManager Members

        public string GetConsumerSecret(string consumerKey)
        {
            if (consumerKey == this.ConsumerKey)
            {
                return this.ConsumerSecret;
            }
            else
            {
                throw new ArgumentException("Unrecognized consumer key.", "consumerKey");
            }
        }

        public string GetTokenSecret(string token)
        {
            return this.tokensAndSecrets[token];
        }

        public void StoreNewRequestToken(UnauthorizedTokenRequest request, ITokenSecretContainingMessage response)
        {
            this.tokensAndSecrets[response.Token] = response.TokenSecret;
        }

        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
            this.tokensAndSecrets.Remove(requestToken);
            this.tokensAndSecrets[accessToken] = accessTokenSecret;
        }

        /// <summary>
        /// Classifies a token as a request token or an access token.
        /// </summary>
        /// <param name="token">The token to classify.</param>
        /// <returns>Request or Access token, or invalid if the token is not recognized.</returns>
        public TokenType GetTokenType(string token)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IOpenIdOAuthTokenManager Members

        public void StoreOpenIdAuthorizedRequestToken(string consumerKey, AuthorizationApprovedResponse authorization)
        {
            this.tokensAndSecrets[authorization.RequestToken] = string.Empty;
        }

        #endregion
    }
}