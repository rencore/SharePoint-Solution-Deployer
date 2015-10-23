Calling Websites
======================================================

Use this extension to call Websites and Webservices.

You can configure the URL, the Method, Payload and Text that should/should not be within the reponse.

Attributes of the &lt;Website&gt; element:

- Url: The URL that will be called
- Method: The Http method ('GET' or 'POST')
- Payload: The string will be appended to the URL (e.g. '?parameter=value', '/rest/like/syntax')
- ContainsText: The response has to contain this string, for the test to be successfull
- NotContainsText: The response must not contain this string, for the test to be successfull

Examples:

- &lt;Website Url="http://spsd.codeplex.com" Method="GET" Payload="" ContainsText="Sharepoint Solution Deployer" NotContainsText="" /&gt;
- &lt;Website Url="http://ip.jsontest.com" Method="GET" Payload="" ContainsText="ip" NotContainsText="Should not contain this text" /&gt;
- &lt;Website Url="http://echo.jsontest.com/key" Method="GET" Payload="/one/two" ContainsText="one" NotContainsText="" /&gt;
- &lt;Website Url="http://validate.jsontest.com" Method="POST" Payload="json={'key':'value'}" ContainsText="error" NotContainsText="" /&gt;
