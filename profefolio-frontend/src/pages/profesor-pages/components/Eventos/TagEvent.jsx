import { BsCircleFill } from "react-icons/bs"
import { TypeEventTarget } from "../ComponentStyles/ComponentsEvent"
import Tools from "../../helpers/Tools.js"

const TagEvent = ({ color, name }) => {
    return <TypeEventTarget >
        <BsCircleFill style={{ color: Tools.GetColorEvento(name), width: "20px", height: "20px" }} />{name}
    </TypeEventTarget>
}

export default TagEvent