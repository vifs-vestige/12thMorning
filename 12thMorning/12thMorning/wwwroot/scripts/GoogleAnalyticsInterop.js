window.dataLayer = window.dataLayer || [];
window.gtag = window.gtag || function () { dataLayer.push(arguments); };
gtag("js", new Date());
var GoogleAnalyticsInterop;
(function (GoogleAnalyticsInterop) {
    function configure(trackingId) {
        const script = document.createElement("script");
        script.async = true;
        script.src = "https://www.googletagmanager.com/gtag/js?id=" + trackingId;
        document.head.appendChild(script);
        gtag("config", trackingId);
    }
    GoogleAnalyticsInterop.configure = configure;
    function navigate(trackingId, href) {
        gtag("config", trackingId, { page_path: href });
    }
    GoogleAnalyticsInterop.navigate = navigate;
})(GoogleAnalyticsInterop || (GoogleAnalyticsInterop = {}));
