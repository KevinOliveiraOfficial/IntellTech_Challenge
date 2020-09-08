# IntellTech_Challenge

# Link da API para criação ou listagem de diretórios:
> /api/directory

Estrutura de dados para criação de Diretórios (método POST)

*Enviar dados em JSON*
  
{

  "directory_name": *string não-vazio*

}

# Link da API para criação ou listagem de Formas Geométricas:
> /api/geometric_shape
Estrutura de dados para criação de Formas Geométricas (método POST)
> Enviar dados em JSON
  
{
  "geometric_shape_directory_guidd": <string contendo o guid do diretório>,
  "geometric_shape_name": <string não-vazio>,
  "geometric_shape_type": <inteiro com valor 1 ou 2>,
  "geometric_shape_color": <string não-vazio>,
  "geometric_shape_pixels": <inteiro diferente de 0>
}

Para listagem de ambos os dados, basta dar um GET nas URLs apresentadas acima.
