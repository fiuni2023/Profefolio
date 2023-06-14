import styled from 'styled-components';
import { ModalHeader } from 'react-bootstrap';

const H1 = styled.h1`
    font-family: 'Poppins';
    font-style: normal;
    font-weight: 600;
    font-size: 16px;
    color: #000000;
`;

const Text = styled.div`
    font-family: 'Poppins';
    font-style: normal;
    font-size: 10pt;
    color: #282828;
`;  

const SHeader = styled(ModalHeader)`
    padding-bottom: 5px;
`;

export {H1, Text, SHeader};    