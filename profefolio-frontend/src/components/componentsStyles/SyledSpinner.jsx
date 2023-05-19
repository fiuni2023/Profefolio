import React from 'react';
import { TbLoader2 } from 'react-icons/tb';
import styled, { keyframes } from 'styled-components';

const spinAnimation = keyframes`
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
`;

const SpinnerDiv = styled.div`
  display: inline-block;
  width: 40px;
  height: 40px;
  animation: ${spinAnimation} 1s linear infinite;
`;

const SpinnerContainer = styled.div`
    width: ${(props) => props.width};
    height: ${(props) => props.height};
    display: flex;
    align-items: center;
    justify-content: center;
`;

const Spinner = ({ width = 50, height = 50 }) => {
    return (
        <SpinnerContainer width={width} height={height}>
            <SpinnerDiv>
                <TbLoader2 size={40} color='#F0544F' />
            </SpinnerDiv>
        </SpinnerContainer>
    );
};

export default Spinner;