import styled from 'styled-components';
import { Container, FormCheck, FormGroup, FormLabel, FormSelect } from 'react-bootstrap';

const SInput = styled.input`
    border-radius: 10px;
    background-color: #E4E4E4;
    color: #282828;
    font-size: 1.2em; 
    font-weight: 500;
    ::placeholder{
        color: #6C6C6C;  
        font-size: 1.2em; 
    }
`;


const SContainer = styled(Container)`
    padding: 0 0.5em;
`;

const SRow = styled.div`
    display: flex;
    justify-content: flex-end;
    padding: 1rem 0 0 0;
`;

const Info = styled.div`
    font-size: 1.2em;
    color: #282828;
    margin-left: calc(var(--bs-gutter-x) * .5);
`;


const SGroup = styled(FormGroup)`
    padding: 0;
`;

const SSelect = styled(FormSelect)`
    border-radius: 10px;
    background-color: #E4E4E4;
    color: #282828;
    font-size: 1.2em; 
    font-weight: 500;
    ::placeholder{
        color: #6C6C6C;  
        font-size: 1.2em; 
    }
`;

const SLabel = styled(FormLabel)`
    font-size: 1.2em;
    color: #282828;
    margin: 0;
    padding: .375rem 0.75rem .1rem 0.75rem;
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

const SOption = styled.option`
    color: #282828;  
    font-size: 1.2em; 
`;

export {SContainer, SRow, Info, SInput, SLabel, SSelect, SCheck, SGroup, SOption, SDOption};