import styled from 'styled-components';
import { Card, Col, FormSelect } from 'react-bootstrap';

const getPointer = (hover) => {
    if (hover === "true") return "pointer";
    else return "default";
}

const getHover = (hover) => {
    if (hover === "true") {
        return `:hover{
            transform: scale(1.03); 
        }`
    }
}

//Ver como pasarle el alto disponible en el viewPort
const SCard = styled(Card)`
    margin: 10px;
    border-radius: 20px;
    border: none;
    box-shadow: 0 0 10px 5px rgba(0, 0, 0, 0.25);
    cursor: ${({ hover }) => getPointer(hover)};
    transition: all .3s ease-in-out;
    ${({ hover }) => getHover(hover)}
    width: 616px; 
    height: 100%; 
`;
const SHeader = styled(Card.Header)`
    background-color: white;
    border-bottom: solid 1px #C2C2C2;
    font-size: 1.5em;
    font-weight: 400;
    padding-left: 20px;
    border-radius: 20px 20px 0 0 !important;
`;
const SBody = styled(Card.Body)`
    background-color: white;
    text-align: center;
    font-size: 1.2em;
    max-height: 400px;
    height: min-content;
    overflow-y: auto;  
    &::-webkit-scrollbar {
        width: 15px;
        height: 15px;
        background-color: transparent;
    }
    &::-webkit-scrollbar-thumb {    
        background-color: rgba(0, 0, 0, 0.1);
    }
    
`;

const SCol = styled(Col)`
  display: flex; 
`;

const SForm = styled.form`
    background-color: white;
    border-top: solid 1px #C2C2C2;
    font-size: 1.3em;
    font-weight: 300;
    padding: 20px;
    padding-top: 10px;
    border-radius: 0 0 20px 20px !important;
`;
const Select = styled(FormSelect)`
    background-color: #F5F5F5;
    border: none;
    font-size: 1.2em;
    font-weight: 400;
    border-radius: 5px;
    width: 100%;
    margin-bottom: 10px;
`;

const List = styled.ol`
  list-style-type: none;
  margin: 10px 0;
  padding: 0;
`;

const Item = styled.li`
  font-size: 0.9em;
  font-weight: 400;
  text-align: left;
`;

const handleColorType = (props) => {
    switch (props.type) {
        case "reload":
            return "#F3E6AE";
        case "new":
            return "#D1F0E6";
        default:
            return "#F5F5F5";
    }
  };
const ItemContainer = styled.div`
    background-color: ${handleColorType};
    border: solid 0.5px #C2C2C2;
    border-radius: 15px;
    width: 100%;
    margin-bottom: 6px;
    text-align: left;
    padding: 3px;
    padding-left: 10px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    position: relative;
}
`;
const ListButton = styled.button`
    cursor: pointer;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size:0.9em;
    font-weight:300;
    width: 22px;
    height: 22px;
    border: none;
    border-radius: 15px;
    background-color: transparent;
    &:hover{
        transform: scale(1.2);
    }
    &:active{
        background-color: #C2C2C2;
    }
}
`;
export { SCard, SHeader, SBody, SCol, SForm, Select, Item, List, ItemContainer, ListButton }; 