import styled from 'styled-components';
import { Container, FormCheck, FormGroup, Col } from 'react-bootstrap';

const SInput = styled.input`
    border-radius: 10px;
    background-color: ${(props)=>props.disabled?'#E4E4E4':'#FFFFFF'} ;
    color: #282828;
    font-size: 1.2em; 
    font-weight: 500;
    ::placeholder{
        color: #6C6C6C;  
        font-size: 1.2em; 
    }
    padding: .1rem 0.75rem;
`;

const SSelect = styled.select`
    border-radius: 10px;
     background-color: ${(props)=>props.disabled?'#E4E4E4':'#FFFFFF'} ;
    color: #282828;
    font-size: 1.2em; 
    font-weight: 500;
    ::placeholder{
        color: #6C6C6C;  
        font-size: 1.2em; 
    }
    padding: .1rem 0.75rem;
`;

const SCol = styled(Col)`
    display: grid;
    padding: 0 10px;
`;

const SCol2 = styled(Col)`
    padding: 0 10px;
    display: flex;
    justify-content: flex-end;
`;

const SLabel = styled.label`
    font-size: 1.2em;
    color: #282828;
    margin: 0;
    padding: .375rem 0.75rem .1rem 0.75rem;
`;
const SRow = styled.div`
    display: flex;
    justify-content: flex-start;
`;
const SOption = styled.option`
    color: #282828;  
    font-size: 1.2em; 
`;

const SClose = styled.div`
    display: flex;
    justify-content: space-between;
    width: 100%;
    padding: 0 10px;
`;

const H1 = styled.h1`
    font-family: 'Poppins';
    font-style: normal;
    font-weight: 600;
    font-size: 1.8em;
    color: #000000;
`;


const SContainer = styled(Container)`
    padding: 0 0.5em;
`;



const Info = styled.div`
    font-size: 1.2em;
    color: #282828;
    margin-left: calc(var(--bs-gutter-x) * .5);
`;


const SGroup = styled(FormGroup)`
    padding: 0;
`;


const SCheck = styled(FormCheck)`
    font-size: 1.2em;
    color: #282828;
    margin: 0;
    margin-left: calc(var(--bs-gutter-x) * .5);
    
    .form-check-input:valid{
        color: #282828 !important;
        border-color: #A6A6A6 !important;
    }

    .form-check-input:hover{
        border-color: #F5918E !important;
    }

    .form-check-label{
        color: #282828 !important;
    }

    
    .form-check-input:checked{
        background-color: #F0544F !important;
        border-color: #F5918E !important;
    }
    
`;

const SDOption = styled.option`
    color: #6C6C6C;  
    font-size: 1.2em; 
`;



export {H1, SContainer, SRow, SCol,SCol2, Info, SInput, SLabel, SSelect, SCheck, SGroup, SOption, SDOption, SClose};