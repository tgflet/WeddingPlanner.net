// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function(){
    $("#location").on("click",function(){
        locating();
    });
});

// $(document).ready(function(){
//     $("#submit").on("click",function(){
//         createlocation();
//     });
// });




function locating(){
$.ajax({
    url: "/newlocation",
    method: "Get",
    success: function(response){
        $("#change").html(response);
    }
});
}
// function createlocation(){
//     $.ajax({
//         url: "/createlocation",
//         method: "Post",
//         success: function(response){
//             $("#change").html(response);
//         }
//     });
//     }
// $(function() {
//     $("#submit").submit(function(event) {
//         event.preventDefault(); // Prevent the form from submitting via the browser
//         var form = $(this);
//         $.ajax({
//         type: form.attr('Post'),
//         url: form.attr('/createlocation'),
//         data: form.serialize()
//         }).done(function(data) {
//         // Optionally alert the user of success here...
//         }).fail(function(data) {
//         // Optionally alert the user of an error here...
//         });
//     });
//     });