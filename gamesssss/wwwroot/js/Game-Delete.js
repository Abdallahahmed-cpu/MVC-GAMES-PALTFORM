$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);

        const deleteBtn = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-danger mx-2",
                cancelButton: "btn btn-light"
            },
            buttonsStyling: false
        });

       
        deleteBtn.fire({
            title: "Are you sure That You Need To Delete This Game?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
        }).then((result) => {
            console.log(result.isConfirmed);
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Games/Delete/${btn.data('id')}`,
                    method: 'DELETE',  // Use 'DELETE' in uppercase for the method
                    success: function () {
                        deleteBtn.fire({
                            title: "Deleted!",
                            text: "Game has been deleted.",
                            icon: "success"
                        });
                        btn.parents('tr').fadeOut();
                    },
                    error: function () {
                        deleteBtn.fire({
                            title: "OOOOOOPS........!",
                            text: "Something Went Wrong.",
                            icon: "error"
                        });
                    }
                });
              
            } 
        });
       
    });
});
//else if (
//    /* Read more about handling dismissals below */
//    result.dismiss === Swal.DismissReason.cancel
//) {
//    swalWithBootstrapButtons.fire({
//        title: "Cancelled",
//        text: "Your imaginary file is safe :)",
//        icon: "error"
//    });
//}