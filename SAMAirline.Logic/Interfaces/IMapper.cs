namespace SAMAirline.Logic.Interfaces
{
    public interface IMapper<TDtoEntity, TDalEntity>
    where TDtoEntity : class
    where TDalEntity : class
    {
        /// <summary>
        /// Maps the DAL entity to the DTO entity. 
        /// </summary>
        /// <param name="dalEntity">
        /// The DAL entity.
        /// </param>
        /// <returns>
        /// The Instance of the <see cref="TDtoEntity"/>.
        /// </returns>
        TDtoEntity MapToDtoEntity(TDalEntity dalEntity);

        /// <summary>
        /// Maps the DTO entity to the DAL entity. 
        /// </summary>
        /// <param name="dtoEntity">
        /// The DTO entity.
        /// </param>
        /// <returns>
        /// The Instance of the <see cref="TDalEntity"/>.
        /// </returns>
        TDalEntity MapToDalEntity(TDtoEntity dtoEntity);
    }
}
