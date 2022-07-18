export const dropzoneConfig = {
    paramName: "file",
    clickable: true,
    url: "",
    method: "POST",
    maxFilesize: 5,
    maxFiles: 5,
    dictResponseError: "Ha ocurrido un error en el servidor",
    acceptedFiles: ".json",
    autoProcessQueue: false,
    parallelUploads: 1,
    uploadMultiple: false,
    chunking: false,
    dictFileTooBig:
        "El archivo es muy grande ({{filesize}}) para cargarlo en el sistema. Capacidad maxima {{maxFilesize}}MB",
    dictUploadCanceled: "La carga de archivos ha sido cancelada.",
    dictInvalidFileType: 'El archivo contiene una extensión no permitida.',
    timeout: 120000,
    createImageThumbnails: false,
    previewsContainer: false,
    successmultiple: () => { },
    errormultiple: () => { }
};