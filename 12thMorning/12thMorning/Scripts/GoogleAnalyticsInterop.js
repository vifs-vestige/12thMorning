window.dataLayer = window.dataLayer || [];
window.gtag = window.gtag || function () { dataLayer.push(arguments); };
gtag("js", new Date());
var GoogleAnalyticsInterop;
(function (GoogleAnalyticsInterop) {
    function configure(trackingId) {
        console.log('test');
    }
    GoogleAnalyticsInterop.configure = configure;
    function navigate(trackingId, href) {
        console.log('test2');
    }
    GoogleAnalyticsInterop.navigate = navigate;
})(GoogleAnalyticsInterop || (GoogleAnalyticsInterop = {}));
//# sourceMappingURL=GoogleAnalyticsInterop.js.map