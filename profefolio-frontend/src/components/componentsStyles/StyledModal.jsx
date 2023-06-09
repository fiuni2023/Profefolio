import styled from 'styled-components';
import { ModalHeader } from 'react-bootstrap';

const H1 = styled.h1`
    font-family: 'Poppins';
    font-style: normal;
    font-weight: 600;
    font-size: 1.8em;
    color: #000000;
`;

const H2 = styled.h2`
    font-family: 'Poppins';
    font-style: normal;
    font-weight: 600;
    font-size: 1em;
    text-align: left;
    color: #000000;
`

const Text = styled.div`
    font-family: 'Poppins';
    font-style: normal;
    font-size: 1.2em;
    color: #282828;
`;  

const SHeader = styled(ModalHeader)`
    padding-bottom: 5px;
`;

export {H1, H2, Text, SHeader};    