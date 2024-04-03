mergeInto(LibraryManager.library, {
    UpdateFileSystem: function() {
        _JS_FileSystem_Sync();
    }
});