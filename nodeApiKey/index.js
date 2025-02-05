import renderApi from '@api/render-api';

renderApi.auth('rnd_hyTvXdm4ml2CnTrAo9aN20niPEVn');
renderApi.listServices({includePreviews: 'true', limit: '20'})
  .then(({ data }) => console.log(data))
  .catch(err => console.error(err));