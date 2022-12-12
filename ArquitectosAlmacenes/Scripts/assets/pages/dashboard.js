
/*
 Template Name: Zoogler - Bootstrap 4 Admin Dashboard
 Author: Mannatthemes
 Website: www.mannatthemes.com
 File: Morris init js
 */





//line-chart



  //Bar
    
 var ctx = document.getElementById("bar-data").getContext('2d');

   
 var gradientStroke1 = ctx.createLinearGradient(0, 0, 0, 300);
     gradientStroke1.addColorStop(0, '#5ecbf6');  
     gradientStroke1.addColorStop(1, '#8d44ad'); 
 
     var cornerRadius = 20;

     var myChart = new Chart(ctx, {
       type: 'bar',        
       data: {
         labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ,11, 12],
         datasets: [{
           label: 'Revenue',
           data: [72, 75, 72, 77, 78, 74, 71, 72, 71, 69, 72, 75],
           borderColor: gradientStroke1,
           backgroundColor: gradientStroke1,
           hoverBackgroundColor: gradientStroke1,
           pointRadius: 0,
           fill: false,
           borderWidth: 0
         },]
       },
       
       options: {
         
         legend: {
           position: 'bottom',
           display:false
         },
         tooltips: {
           displayColors:false,
           intersect: false,
         },
         scales: {
           xAxes: [{
               ticks: {
                   max: 100,
                   min: 20,
                   stepSize: 10                        
               },
               gridLines: {
                   display: false ,
                   color: "#FFFFFF"
               },
               ticks: {
                display: true,
                fontFamily: "'Rubik', sans-serif"
                },
               
           }],
           yAxes: [{                   
               gridLines: {
                 color: '#fff',
                 display: false ,
               },
               ticks: {
                   display: false,
                   fontFamily: "'Rubik', sans-serif"
               },
               
           }]
       },
      }
     });


     $(document).ready(function() {
      $(".boxscroll").niceScroll({cursorborder:"",cursorcolor:"#eff3f6",boxzoom:true});
    }); 

     
