﻿
    $('.sidebar-menu li a').each(function () {
        if ($($(this))[0].href == String(window.location)) {
            $(this).parent().addClass('active');
            $(this).parent().parent().parent().addClass('active');
        }
    })
