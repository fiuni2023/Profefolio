import styled from 'styled-components';

const ContainerBlock = styled.div`
    background: #FFFFFF;
    box-shadow: 4px 4px 10px 5px rgba(0, 0, 0, 0.25);
    border-radius: 20px;
    padding: 1rem 1.5rem;
`;

const STitle = styled.header`
    font-weight: 400;
    font-size: 24px;
    line-height: 36px;
    padding: 0px 0.75rem;
`

const SRowContainer = styled.div`
    background: #FFFFFF;
    box-shadow: 4px 4px 10px 5px rgba(0, 0, 0, 0.25);
    border-radius: 20px;
    display: block;
    border: 1px solid gray;
    min-height: 500px;
`

const SRowContainerHeader = styled.div`
    border-bottom: 2px solid black;
    padding: 0.5rem 1.5rem;
`


export { ContainerBlock, STitle, SRowContainer,SRowContainerHeader };