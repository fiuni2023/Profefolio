import React from "react";
import Card from "../../../../../components/Card";

const AnotacionCard = ({
    observacion = {Titulo: "Titulo Base", Contenido: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam at hendrerit nulla. In ullamcorper augue sem, ac consectetur magna dictum sed. Curabitur commodo, ligula ultricies rhoncus egestas, orci mauris bibendum sapien, at viverra mi massa id ex. Lorem ipsum dolor sit amet, consectetur adipiscing elit."},
    onClick = () => {}
}) => {

    const text = observacion?.Contenido?.length > 100 ? `${observacion.Contenido.substring(0, 100)}...` : observacion.Contenido

    const configuracion = {
        xs: 12, sm: 12, md: 12, lg: 12,
        background: "orange",
        hover: true,
        action: () => onClick(observacion),
        header: {
            title: `${observacion?.Titulo}`,
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

export default AnotacionCard