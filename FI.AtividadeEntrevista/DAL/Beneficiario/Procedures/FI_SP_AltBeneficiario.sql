﻿CREATE PROC FI_SP_AltBeneficiario
    @NOME          VARCHAR (50) ,
	@CPF           VARCHAR (14) ,
	@CLIENTEID     BIGINT,
	@Id            BIGINT
AS
BEGIN
	UPDATE BENEFICIARIOS 
	SET 
		NOME = @NOME, 
		CPF = @CPF,
		CLIENTEID = @CLIENTEID
	WHERE Id = @Id
END