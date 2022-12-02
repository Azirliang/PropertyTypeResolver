using AutoMapper;
using DtoPropertyTypeResolver.Dto;
using DtoPropertyTypeResolver.EfContext;
using DtoPropertyTypeResolver.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DtoPropertyTypeResolver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : ControllerBase
    {
        private readonly ILogger<TemplateController> _logger;

        private TestContext _testContext;

        private IMapper _mapper;

        public TemplateController(ILogger<TemplateController> logger, TestContext testContext, IMapper mapper)
        {
            _logger = logger;
            _testContext = testContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 新增数据
        /// DTO 和 Entity 之间利用AutoMapper转换<see href="IPropertyType"/>查看此 <see href="TemplateMapper"/> 类
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TemplateEntity> Post([FromBody] TemplateDto dto, CancellationToken cancellationToken)
        {
            // 可使用该测试数据：
            //{
            //    "id": 1,
            //    "name": "string",
            //    "templateValue": {
            //        "propertyType": "StringValue",
            //        "value":"azir"
            //    }
            //}

            // or 

            //{
            //    "id": 2,
            //  "name": "range",
            //  "templateValue": {
            //                    "propertyType": "RangeValue",
            //    "minValue":1,
            //    "maxValue":2
            //     }
            //}


            int id = dto.Id;

            var entity = _mapper.Map<TemplateEntity>(dto);

            _testContext.Add(entity);

            await _testContext.SaveChangesAsync(cancellationToken);

            await _testContext.DisposeAsync();

            return entity;
        }

        /// <summary>
        /// 查询数据
        /// DTO 和 Entity 之间利用AutoMapper转换<see href="IPropertyType"/>查看此 <see href="TemplateMapper"/> 类
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<TemplateDto> Get(int id, CancellationToken cancellationToken)
        {
            var entity = await _testContext.FindAsync<TemplateEntity>(id);

            if (entity == null)
            {
                entity = new TemplateEntity
                {
                    Id = id,
                    Name = "字符串类型",
                    PropertyType = "StringValue",
                    TemplateValue = JsonConvert.SerializeObject(new StringValue
                    {
                        Value = "测试字符串类型"
                    })
                };

                _testContext.Add(entity);

                await _testContext.SaveChangesAsync(cancellationToken);
            }

            var dbEntity = await _testContext.FindAsync<TemplateEntity>(id);

            var dto = _mapper.Map<TemplateDto>(dbEntity);

            return dto;
        }
    }
}