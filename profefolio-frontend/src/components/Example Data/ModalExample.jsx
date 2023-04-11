const ModalExample = {
    header : "Modal de prueba",
    text : "Texto de prueba para ver como queda y estilizar",
    form : {
      inputs  : [
        {
          xs: 6, sm: 6, md: 6, lg: 6,
          key: "nombre", label: "Nombre",
          type: "text", placeholder: "Ingrese el nombre",
          disabled: false,
          required: true,
          invalidText: "Ingrese un nombre",
        },
        {
          xs: 6, sm: 6, md: 6, lg: 6,
          key: "contrasenia", label: "Contraseña",
          type: "password", placeholder: "contraseña",
          disabled: false,
          required: true,
        },
        {
          xs: 6, sm: 6, md: 6, lg: 6,
          key: "fecha", label: "Fecha",
          type: "date", placeholder: "Seleccione la fecha",
          disabled: false,
          required: true,
        },
        {
            key: "select-ex", label: "Opciones",
            type: "select", 
            disabled: false,
            select: {
                default: "Seleccione una opcion",
                options: [
                    {
                        value: 1,
                        text: "Opcion 1"
                    }, 
                    {
                        value: 2,
                        text: "Opcion 2"
                    }, 
                    {
                        value: 3,
                        text: "Opcion 3"
                    }, 
                ]
            }
        }, 
        {
            key: "check", label: "Otras opciones checkbox",
            type: "checkbox",
            disabled: false,
            checks: [
                {
                    xs: 6, sm: 6, md: 6, lg: 6,
                    id: "option-1",
                    label: "Opcion 1",
                    value: 1, 
                },  
                {
                    xs: 6, sm: 6, md: 6, lg: 6,
                    id: "option-2",
                    label: "Opcion 2",
                    value: 2,
                },
                {
                    xs: 12, sm: 12, md: 12, lg: 12,
                    id: "option-3",
                    label: "Opcion 3",
                    value: 3,
                },  
            ]
        },
        {
            key: "radio", label: "Otras opciones radio",
            type: "radio",
            disabled: false,
            checks: [
                {
                    xs: 4, sm: 4, md: 4, lg: 4,
                    id: "option-4",
                    label: "Opcion 1",
                    value: 4, 
                },  
                {
                    xs: 4, sm: 4, md: 4, lg: 4,
                    id: "option-5",
                    label: "Opcion 2",
                    value: 5,
                },
                {
                    xs: 4, sm: 4, md: 4, lg: 4,
                    id: "option-6",
                    label: "Opcion 3",
                    value: 6,
                },  
            ]
        },
        {
            key: "switch", label: "Otras opciones switch",
            type: "switch",
            disabled: false,
            checks: [
                {
                    id: "option-7",
                    label: "Opcion 1",
                    value: 7,
                },  
                {
                    id: "option-8",
                    label: "Opcion 2",
                    value: 8,
                },
                {
                    id: "option-9",
                    label: "Opcion 3",
                    value: 9,
                },  
            ]
        },
       

      ],
      buttons : [
        {
            style: "text",
            type: "accept",
            onclick: { action: (() => console.log("aceptar")) },
            submit: true,
        },
        {
            style: "text",
            type: "cancel",
            onclick: { action: (() => console.log("cancelar")) },
        },
        {
            style: "icon",
            type: "edit",
            onclick: { action: (() => console.log("edit")) },
        },
      ],
      info : "Info del formulario"
    }
}

export default ModalExample;