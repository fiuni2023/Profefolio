import React from "react";
import Card from "../../../../components/Card";

const CardAnotaciones = ({
    observacion = { titulo: "Titulo Base", contenido: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam at hendrerit nulla. In ullamcorper augue sem, ac consectetur magna dictum sed. Curabitur commodo, ligula ultricies rhoncus egestas, orci mauris bibendum sapien, at viverra mi massa id ex. Lorem ipsum dolor sit amet, consectetur adipiscing elit." },
    onClick = () => { },
    hover = true
}) => {

    const text = observacion?.contenido?.length > 100 ? `${observacion.contenido.substring(0, 100)}...` : observacion.contenido

    const configuracion = {
        xs: 12, sm: 12, md: 12, lg: 12,
        background: "orange",
        hover: hover,
        action: () => onClick(observacion),
        header: {
            title: `${observacion?.titulo}`,
        },
        body: {
            first: {
                title: `${text}`
            }
        }
    }

    return <>
        <Card cardInfo={configuracion} />
    </>
}

export default CardAnotaciones