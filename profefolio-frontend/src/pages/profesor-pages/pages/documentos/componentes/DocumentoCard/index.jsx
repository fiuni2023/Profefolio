import React from "react";
import Card from "../../../../../../components/Card";

const DocumentoCard = ({
    documento = {
        id: 1,
        nombre: "a",
        link: "https://www.youtube.com"
    },
    onClick = () => { }
}) => {

    const configuracion = {
        xs: 12, sm: 12, md: 12, lg: 12,
        hover: true,
        action: ()=>{onClick(documento)},
        header: {
            title: `${documento.nombre}`,
        },
        body: {
            first: {
                title: `${documento.link}`
            }
        }
    }

    return <>
        <Card cardInfo={configuracion} />
    </>
}

export default DocumentoCard