/*
    Carousel
*/

$('#carousel-example').on('slide.bs.carousel', function (e) {
    /*
        CC 2.0 License Iatek LLC 2018 - Attribution required
    */
    var $e = $(e.relatedTarget);
    var idx = $e.index();
    var itemsPerSlide = 4;
    var totalItems = $('.carousel-item').length;

    if (idx >= totalItems - (itemsPerSlide - 1)) {
        var it = itemsPerSlide - (totalItems - idx);
        for (var i = 0; i < it; i++) {
            // append slides to end
            if (e.direction == "left") {
                $('.carousel-item').eq(i).appendTo('.carousel-inner');
            }
            else {
                $('.carousel-item').eq(0).appendTo('.carousel-inner');
            }
        }
    }
});
(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

ga('create', 'UA-46156385-1', 'cssscript.com');
ga('send', 'pageview');

$(document).ready(function () {
    $('#characterLeft').text('140 characters left');
    $('#message').keydown(function () {
        var max = 140;
        var len = $(this).val().length;
        if (len >= max) {
            $('#characterLeft').text('You have reached the limit');
            $('#characterLeft').addClass('red');
            $('#btnSubmit').addClass('disabled');
        }
        else {
            var ch = max - len;
            $('#characterLeft').text(ch + ' characters left');
            $('#btnSubmit').removeClass('disabled');
            $('#characterLeft').removeClass('red');
        }
    });
});
function ChangeMap(id) {
    switch (id) {
        case 3:
            document.getElementById('M1').src = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d272886.8623484848!2d11.613661266434978!3d57.700682649121255!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x464f8e67966c073f%3A0x4019078290e7c40!2zR8O2dGVib3Jn!5e0!3m2!1ssv!2sse!4v1634914188978!5m2!1ssv!2sse";
            break;
        case 2:
            document.getElementById('M1').src = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d136235.28504714725!2d14.048172200679987!3d57.75591977724705!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x46508031565bd12d%3A0x8b01be57dcc2f66d!2zSsO2bmvDtnBpbmc!5e0!3m2!1ssv!2sse!4v1634915850292!5m2!1ssv!2sse";
            break;
        case 1:
            document.getElementById('M1').src = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d130266.39963762637!2d17.841970985873292!3d59.32606681374197!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x465f763119640bcb%3A0xa80d27d3679d7766!2sStockholm!5e0!3m2!1ssv!2sse!4v1634915959558!5m2!1ssv!2sse";
            break;

    }

};

