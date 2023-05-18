import React from "react";
import Card from "../../../../components/Card";
import { toast } from "react-hot-toast";

const AnotationCard = ({
    anotation = {type: "alg"}
}) => {
    // colores = ["yellow", "blue", "purple", "orange"]
    const getColor = (type) => {
        switch(type){
            case "algo":
                return "yellow"
            default:
                return "orange"
        }
    }

    const handleClickAnotation = () => {
        toast.success("este success")
    }

    const texto = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum sed porttitor justo, a rutrum augue. Vivamus posuere eros nisi, eu laoreet arcu tincidunt eu. Vestibulum eleifend aliquam lobortis. Vestibulum non erat porta, mollis ante ut, molestie diam. Nulla non neque est. Praesent congue massa sollicitudin sapien facilisis, et pulvinar diam congue. Cras convallis varius dolor, vel facilisis lacus hendrerit id. Maecenas lacinia nibh a nisi sollicitudin, in pretium risus aliquet. Morbi vitae risus quam. Mauris dolor augue, tempus eget pellentesque ac, ornare quis purus. Duis mollis est ac neque porttitor, bibendum aliquet dui condimentum. Ut nec mi consequat, maximus dolor in, suscipit ante."

    const config = {
        xs: 12, sm: 12, md: 12, lg: 12,
        background: getColor(anotation.type),
        hover: true,
        action: handleClickAnotation,
        header: {
            title: `Titulo`,
        },
        body: {
            first: {
                title: texto.length > 75? `${texto.substring(0,75)}...` : texto
            }
        }
    }

    return <>
        <div className="w-100 my-3">
            <Card cardInfo={config} />
        </div>
    </>

}

export default AnotationCard