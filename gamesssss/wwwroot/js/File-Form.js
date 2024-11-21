$(document).ready(function () {
    $('#Cover').change(function () {
        if (this.files && this.files[0]) {
            $('.cover-preview').attr('src', window.URL.createObjectURL(this.files[0]));
        }
    });
});