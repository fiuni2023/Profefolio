import styled from 'styled-components';
import { Card } from 'react-bootstrap';

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

const SCard = styled(Card)`
    border-radius: 20px;
    border: none;
    transition: all .2s ease-in-out; 
    hover:{
        transform: scale(1.1); 
    }
`;
const SHeader = styled(Card.Header)`
    background-color: ${({ background }) => handleColorType(background, true)};
    border: none;
    font-size: 1.5em;
    font-weight: 600;
    text-align: center;
`;
const SBody = styled(Card.Body)`
    background-color: ${({ background }) => handleColorType(background, false)};
    border: none;
    text-align: center;
    font-size: 1.2em;
`;

const STitle = styled.div`
    margin-bottom: 10px;
    font-weight: 400;

`;

export {SCard, SHeader, SBody, STitle}; 