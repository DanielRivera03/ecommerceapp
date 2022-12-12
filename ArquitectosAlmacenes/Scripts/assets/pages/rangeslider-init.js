/*
 Template Name: Zoogler - Bootstrap 4 Admin Dashboard
 Author: Mannatthemes
 Website: www.mannatthemes.com
 File: Range slider
 */

$(document).ready(function () {
    $("#range_01").ionRangeSlider();
    
    $("#range_02").ionRangeSlider({
        min: 100,
        max: 1000,
        from: 550
    });
    
    $("#range_03").ionRangeSlider({
        type: "double",
        grid: true,
        min: 0,
        max: 1000,
        from: 200,
        to: 800,
        prefix: "$"
    });
   
    $("#range_04").ionRangeSlider({
        type: "double",
        grid: true,
        min: -1000,
        max: 1000,
        from: -500,
        to: 500
    });
    
    $("#range_05").ionRangeSlider({
        type: "double",
        grid: true,
        min: -1000,
        max: 1000,
        from: -500,
        to: 500,
        step: 250
    });
    
    $("#range_06").ionRangeSlider({
        grid: true,
        from: 3,
        values: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"]
    });
    
    $("#range_07").ionRangeSlider({
        grid: true,
        min: 1000,
        max: 1000000,
        from: 200000,
        step: 1000,
        prettify_enabled: true
    });
    
    $("#range_08").ionRangeSlider({
        min: 100,
        max: 1000,
        from: 550,
        disable: true
    });
    $("#range_09").ionRangeSlider({
        grid: true,
        min: 18,
        max: 70,
        from: 30,
        prefix: "Age ",
        max_postfix: "+"
    });
    $("#range_10").ionRangeSlider({
        type: "double",
        min: 100,
        max: 200,
        from: 145,
        to: 155,
        prefix: "Weight: ",
        postfix: " million pounds",
        decorate_both: true
    });
    $("#range_11").ionRangeSlider({
        type: "single",
        grid: true,
        min: -90,
        max: 90,
        from: 0,
        postfix: "Â°"
    });
    $("#range_12").ionRangeSlider({
        type: "double",
        min: 1000,
        max: 2000,
        from: 1200,
        to: 1800,
        hide_min_max: true,
        hide_from_to: true,
        grid: true
    });

     // Step.
     var stp = document.querySelector('.js-step');
     var initStp = new Powerange(stp, { start: 50, step: 10 });
 
     // Min, max, start.
     var vals = document.querySelector('.js-min-max-start');
     var initVals = new Powerange(vals, { min: 16, max: 256, start: 128 });
 
     // Callback.
     var clbk = document.querySelector('.js-callback');
     var initClbk = new Powerange(clbk, { callback: displayValue, start: 88 });
 
     function displayValue() {
     document.getElementById('js-display-callback').innerHTML = clbk.value;
     }
 
     // Hide range.
     var hide = document.querySelector('.js-hiderange');
     var initHideRange = new Powerange(hide, { hideRange: true, start: 70 });
 
     // Vertical.
     var vert = document.querySelector('.js-vertical');
     var initVert = new Powerange(vert, { start: 80, vertical: true });
});