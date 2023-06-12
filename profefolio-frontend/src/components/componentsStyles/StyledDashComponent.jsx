import styled from 'styled-components';
import { Card } from 'react-bootstrap';
import { Col, Row } from 'react-bootstrap';

const handleColorType = (color, header) => {
    switch (color) {
        case "orange":
            return header ? "#F88045" : "#FCC6AC";
        case "purple":
            return header ? "#9C8CBB" : "#C8BFD9";
        case "blue":
            return header ? "#92CAF7" : "#C1E1FA";
        case "yellow":
            return header ? "#F0D567" : "#F6E7A7";    
        default:
            return header ? "#C2C2C2" : "#EEEEEE";
    }
  };

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

const SCard = styled(Card)`
    border-radius: 20px;
    border: none;
    cursor: ${({hover}) => getPointer(hover) };
    transition: all .3s ease-in-out;
    ${({hover}) => getHover(hover)}
    width: 100%; 
`;
const SHeader = styled(Card.Header)`
    background-color: ${({ background }) => handleColorType(background, true)};
    border: none;
    font-size: 16px;
    font-weight: 600;
    text-align: center;
    border-radius: 20px 20px 0 0 !important;
    color: #ffffff;
`;
const SBody = styled(Card.Body)`
    background-color: ${({ background }) => handleColorType(background, false)};
    border: none;
    text-align: center;
    font-size: 10pt;
    border-radius: 0 0 20px 20px;
`;

const STitle = styled.div`
    margin-bottom: 10px;
    font-weight: 400;

`;

const DTitle = styled.h3`
  font-size: 16px;
`

const SCol = styled(Col)`
  display: flex; 
`;

const SRow = styled(Row)`
    --bs-gutter-y: 1.5em;
`;

const Separator = styled.div`
  margin-top: 1.5em;  
`;
const TwoCol = styled.div`
  display: flex;
  gap: 1em;
`;

const FirstCol = styled.div`
  width: 50%;
  font-weight: 600;
  font-size: 10pt;  
  text-align: initial; 
`;

const SecondCol = styled.div`
  width: 50%;
  font-weight: 400;
  font-size: 10pt;  
  text-align: initial; 
`;
const SingleCol = styled.div`
    font-weight: 600;
    font-size: 10pt; 
    text-align: initial; 
`;

const ThreeCol = styled.div`
    display: flex;
    gap: 1em;
    align-items: center;
    margin-top: 10px;
`;

const MainCol = styled.div`
    text-align: initial; 
`;

const SecondaryCol = styled.div`
    margin-left: auto;  
`;

const ClockContainer = styled.div`
    width: 2em;
    height: 2em;
`;

export {SCard, SHeader, SBody, STitle, DTitle, SCol, SRow, Separator, TwoCol, FirstCol, SecondCol, SingleCol, ThreeCol, MainCol, SecondaryCol, ClockContainer}; 