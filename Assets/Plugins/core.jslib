mergeInto(LibraryManager.library, {
    Alert: function(message){
        window.alert(Pointer_stringify(message));
    }
});