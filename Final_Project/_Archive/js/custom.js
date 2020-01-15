//CUSTOM LIGHTCOX
///////////////////CUSTOM LIGHTBOX/////////////////
//When the user clicks an image, display the lightbox-container
//insert an img tag iniside of #lightbox-content
$(".thumb").on("click", function () {

    //find the src attribute of the image taht was clicked
    var imgSrc = $(this).attr('src');//retrieve thse source attribute

    //plug an dimg tag into the #lighbox-content- use the imgsrc as the src attribute
    $('#lightbox-content').html("<img src='" + imgSrc + "'class='img-responsive'/>");

    //make the light box visiable,by fading it in
    $("#lightbox-container").fadeIn(500);

    //when the user clicks anywhere in the #lightbox-container, fadeOut()
    $('#lightbox-container').on('click', function () {
        $(this).fadeOut(500);
    });

});

 // end ready function