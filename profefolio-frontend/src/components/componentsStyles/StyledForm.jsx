import styled from 'styled-components';
import { Container, FormCheck, FormControl, FormGroup, FormLabel, FormSelect, Row } from 'react-bootstrap';

const SContainer = styled(Container)`
    padding: 0 0.5em;
`;

const SRow = styled.div`
    display: flex;
    justify-content: flex-end;
    padding: 1rem 0 0 0;
`;

const SRow2 = styled(Row)`
    padding: 1rem;
    background-color: #fff;
    margin: 1rem 0.5rem;
    border-radius: 15px;
` 

const Info = styled.div`
    font-size: 1em;
    color: #282828;
    margin-left: calc(var(--bs-gutter-x) * .5);
`;

const SControl = styled(FormControl)`
    border-radius: 10px;
    background-color: #E4E4E4;
    color: #282828;
    font-size: 1em; 
    font-weight: 500;
    ::placeholder{
        color: #6C6C6C;  
        font-size: 1em; 
    }
`;

const SGroup = styled(FormGroup)`
    padding: 0;
`;

const SSelect = styled(FormSelect)`
    border-radius: 10px;
    background-color: #E4E4E4;
    color: #282828;
    font-size: 1em; 
    font-weight: 500;
    ::placeholder{
        color: #6C6C6C;  
        font-size: 1em; 
    }
`;

const SLabel = styled(FormLabel)`
    font-size: 1em;
    color: #282828;
    margin: 0;
    padding: .375rem 0.75rem .1rem 0.75rem;
    position: relative !important;
`;

const SCheck = styled(FormCheck)`
    font-size: 1em;
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
    font-size: 1em; 
`;

const SOption = styled.option`
    color: #282828;  
    font-size: 1em; 
`;

export {SContainer, SRow, SRow2, Info, SControl, SLabel, SSelect, SCheck, SGroup, SOption, SDOption};