import styled from 'styled-components';
import { Card, Col, Row, FormSelect} from 'react-bootstrap';

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
    height: calc(min(100% , 800px)); 
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
    border-radius: 0 0 20px 20px;
`;


const DTitle = styled.h3`
  font-size: 1.5em;
`

const SCol = styled(Col)`
  display: flex; 
`;

const SRow = styled(Row)`
    --bs-gutter-y: 1.5em;
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
    font-size: 1.3em;
    font-weight: 400;
    border-radius: 5px;
    width: 100%;
    margin-bottom: 10px;
    height: 30px;
`;
export { SCard, SHeader, SBody, DTitle, SCol, SRow, SForm, Select}; 