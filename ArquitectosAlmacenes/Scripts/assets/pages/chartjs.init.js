/*
 Template Name: Zoogler - Bootstrap 4 Admin Dashboard
 Author: Mannatthemes
 Website: www.mannatthemes.com
 File: Chart js 
 */

 
//line-chart
var ctx = document.getElementById('lineChart').getContext('2d');

gradientStroke1 = ctx.createLinearGradient(0, 0, 0, 300);
   gradientStroke1.addColorStop(0, '#008cff');
   gradientStroke1.addColorStop(1, 'rgba(22, 195, 233, 0.1)');

gradientStroke2 = ctx.createLinearGradient(0, 0, 0, 300);
   gradientStroke2.addColorStop(0, '#ec536c');
   gradientStroke2.addColorStop(1, 'rgba(222, 15, 23, 0.1)');

   var myChart = new Chart(ctx, {
     type: 'line',

     data: {
       labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
       datasets: [{
         label: '1-Dataset',
         data: [3, 30, 10, 10, 22, 12, 5],
         pointBorderWidth: 0,
         pointHoverBackgroundColor: gradientStroke1,
         backgroundColor: gradientStroke1,
         borderColor: 'transparent',
         borderWidth: 1
       },
       {
           label: '2-Dataset',
           data: [5, 15, 12, 25, 5, 7, 5],
           pointBorderWidth:0,
           pointHoverBackgroundColor: gradientStroke2,
           backgroundColor: gradientStroke2,
           borderColor: 'transparent',
           borderWidth: 1
         }],
      
     },
     options: {
         legend: {
           position: 'bottom',
           display:true,
           labels: {
            boxWidth:12
          }
         },
         tooltips: {
           displayColors:true,
           intersect: true,
         },
         elements: {
            point:{
                radius: 3
            }
        },
         scales: {
           xAxes: [{
               ticks: {
                   max: 100,
                   min: 20,
                   stepSize: 10                        
               },
              
               ticks: {
                display: true,
                fontFamily: "'Rubik', sans-serif"
                },
               
           }],
           yAxes: [{                   
              
               ticks: {
                   display: true,
                   fontFamily: "'Rubik', sans-serif"
               },
               
           }]
       },
      }
   });

    
// Pie
var ctx = document.getElementById("dash-pie").getContext('2d');

  var gradientStroke6 = ctx.createLinearGradient(0, 0, 0, 300);
      gradientStroke6.addColorStop(0, '#00e795');
      gradientStroke6.addColorStop(1, '#0095e2');
    
  var gradientStroke7 = ctx.createLinearGradient(0, 0, 0, 300);
      gradientStroke7.addColorStop(0, '#f6d365');
      gradientStroke7.addColorStop(1, '#ff7850');

  var gradientStroke8 = ctx.createLinearGradient(0, 0, 0, 300);
      gradientStroke8.addColorStop(0, '#f56348');
      gradientStroke8.addColorStop(1, '#f81f8b');

      var myChart = new Chart(ctx, {
        type: 'pie',
        data: {
          labels: ["Completed", "Pending", "Process"],
          datasets: [{
            backgroundColor: [
              gradientStroke6,
              gradientStroke7,
              gradientStroke8
            ],

             hoverBackgroundColor: [
              gradientStroke6,
              gradientStroke7,
              gradientStroke8
            ],

            data: [50, 50, 50],
      borderWidth: [0, 0, 0],
          }]
        },
        options: {
            legend: {
              position: 'bottom',
              display: true,
            labels: {
                boxWidth:12
              }
            },
			tooltips: {
			  displayColors:true,
			},
        }
      });




	 
//Doughnut
      
var ctx = document.getElementById("doughnut").getContext('2d');

gradientStroke9 = ctx.createLinearGradient(0, 0, 0, 300);
  gradientStroke9.addColorStop(0, '#00e795');
  gradientStroke9.addColorStop(1, '#0095e2');

gradientStroke10 = ctx.createLinearGradient(0, 0, 0, 300);
  gradientStroke10.addColorStop(1, '#f6d365');
  gradientStroke10.addColorStop(0, '#ff7850');

gradientStroke11 = ctx.createLinearGradient(0, 0, 0, 300);
  gradientStroke11.addColorStop(0, '#f56348');
  gradientStroke11.addColorStop(1, '#f81f8b');

  var myChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
      labels: ["Active", "Panding", "Complete"],
      datasets: [{
        backgroundColor: [
          gradientStroke9,
          gradientStroke10,
          gradientStroke11,
        ],
        hoverBackgroundColor: [
          gradientStroke9,
          gradientStroke10,
          gradientStroke11,
        ],
        data: [22, 25, 25],
        borderWidth: [.8, .8, .8]
      }]
    },
    options: {
        cutoutPercentage: 75,
        legend: {
          position: 'bottom',
          display: true,
          labels: {
            boxWidth:12
          }
      },          
    }
  }); 



   //Bar
    
 var ctx = document.getElementById("bar-data").getContext('2d');

   
 var gradientStroke12 = ctx.createLinearGradient(0, 0, 0, 300);
     gradientStroke12.addColorStop(0, '#5ecbf6');  
     gradientStroke12.addColorStop(1, '#8d44ad'); 
 
     var cornerRadius = 20;

     var myChart = new Chart(ctx, {
       type: 'bar',        
       data: {
         labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ,11, 12],
         datasets: [{
           label: 'Revenue',
           data: [72, 75, 72, 77, 78, 74, 71, 72, 71, 69, 72, 75],
           borderColor: gradientStroke12,
           backgroundColor: gradientStroke12,
           hoverBackgroundColor: gradientStroke12,
           pointRadius: 0,
           fill: true,
           borderWidth: 0
         },]
       },
       
       options: {
         
         legend: {
           position: 'bottom',
           display:true,
           labels: {
            boxWidth:12
          }
         },
         tooltips: {
           displayColors:true,
           intersect: true,
         },
         scales: {
           xAxes: [{
               ticks: {
                   max: 100,
                   min: 20,
                   stepSize: 10                        
               },
               gridLines: {
                   display: true ,
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
                 display: true ,
               },
               ticks: {
                   display: true,
                   fontFamily: "'Rubik', sans-serif"
               },
               
           }]
       },
      }
     });



       
// Polar
var ctx = document.getElementById("polarArea").getContext('2d');

var gradientStroke13 = ctx.createLinearGradient(0, 0, 0, 300);
    gradientStroke13.addColorStop(0, '#42e695');
    gradientStroke13.addColorStop(1, '#3bb2b8');

    var gradientStroke14 = ctx.createLinearGradient(0, 0, 0, 300);
    gradientStroke14.addColorStop(0, '#4776e6');
    gradientStroke14.addColorStop(1, '#8e54e9');


    var gradientStroke15 = ctx.createLinearGradient(0, 0, 0, 300);
    gradientStroke15.addColorStop(0, '#ee0979');
    gradientStroke15.addColorStop(1, '#ff6a00');

    var myChart = new Chart(ctx, {
      type: 'polarArea',
      data: {
        labels: ["Gross Profit", "Revenue", "Expense"],
        datasets: [{
          backgroundColor: [
            gradientStroke13,
            gradientStroke14,
            gradientStroke15
          ],

           hoverBackgroundColor: [
            gradientStroke13,
            gradientStroke14,
            gradientStroke15
          ],
          data: [4, 8, 7]
        }]
      },
      options: {
          legend: {
            display: true,
            position: 'bottom',
            labels: {
                boxWidth:12
              }
          },
         
    scale: {
      gridLines: {
         color: "rgba(221, 221, 221, 0.9)" 
       }, 
    }
      }
    });



    // Polar
var ctx = document.getElementById("radar").getContext('2d');

var gradientStroke13 = ctx.createLinearGradient(0, 0, 0, 300);
    gradientStroke13.addColorStop(0, '#42e695');
    gradientStroke13.addColorStop(1, '#3bb2b8');

    var gradientStroke14 = ctx.createLinearGradient(0, 0, 0, 300);
    gradientStroke14.addColorStop(0, '#4776e6');
    gradientStroke14.addColorStop(1, '#8e54e9');


    var gradientStroke15 = ctx.createLinearGradient(0, 0, 0, 300);
    gradientStroke15.addColorStop(0, '#ee0979');
    gradientStroke15.addColorStop(1, '#ff6a00');

    var myChart = new Chart(ctx, {
        type: 'radar',
    data: {
      labels: ["Africa", "Asia", "Europe", "Latin America", "North America"],
      datasets: [
        {
          label: "1950",
          fill: true,
          backgroundColor: "rgba(251,88,22,0.4)",
          borderColor: gradientStroke15,
          pointBorderColor: "rgba(251,88,22,0.9)",
          pointBackgroundColor: "rgba(251,88,22,0.9)",
          data: [65, 59, 90, 81, 56, 55, 40],
        }, {
          label: "2050",
          fill: true,
          backgroundColor: "rgba(119,94,232,0.4)",
          borderColor: gradientStroke14,
          pointBorderColor: "rgba(119,94,232,0.8)",
          pointBackgroundColor: "rgba(119,94,232,0.9)",
          pointBorderColor: "rgba(119,94,232,0.9)",
         data: [28, 48, 40, 19, 96, 27, 100],
        }
      ]
    },
    options: {
      title: {
        display: true,
        text: 'Distribution in % of world population'
      },
      legend: {
        display: true,
        position: 'bottom',
        labels: {
            boxWidth:12
          }
      },
    }
});