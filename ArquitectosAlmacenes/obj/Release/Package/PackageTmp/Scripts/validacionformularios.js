jQuery(".form-valide").validate({
    rules: {
        "val-txtnombres": {
            required: !0,
            minlength: 5
        },
        
    },
    messages: {
        "val-txtnombres": {
            required: "Por favor ingrese sus nombres",
            minlength: "Sus nombres debe contener al menos 5 carácteres",
            maxlength: "No puede exceder de 255 carácteres",
        }
        
    },
    ignore: [],
    errorClass: "invalid-feedback animated fadeInUp",
    errorElement: "div",
    errorPlacement: function (e, a) {
        jQuery(a).parents(".form-group > div").append(e)
    },
    highlight: function (e) {
        jQuery(e).closest(".form-group").removeClass("is-invalid").addClass("is-invalid")
    },
    success: function (e) {
        jQuery(e).closest(".form-group").removeClass("is-invalid"), jQuery(e).remove()
    },
});